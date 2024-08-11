import { Router } from "express";
import { Pool } from "../DB.js";
import { MsgType, MSG } from "./CommonPacket.js";

export const UserRouter = new Router();

UserRouter.post("/user_register", async (req, res) => {
	let { email, password, name } = req.body;

	const sql =
		"INSERT INTO users (email, password, name) VALUES (?, SHA1(?), ?)";
	try {
		let result = await Pool.query(sql, [email, password, name]);

		MSG.type = MsgType.SUCESS;
		MSG.body = "가입 완료";
		res.json(MSG);
	} catch (err) {
		MSG.type = MsgType.ERROR;
		MSG.body = "서버 오류 발생";
		//TODO : 이거는 나중에 에러 코드 걸러줘도 좋음 근데 귀찮은 작업 일듯
		res.json(MSG);
	}
});

UserRouter.post("/user_login", async (req, res) => {
	let { email, password } = req.body;

	const sql = "SELECT * FROM users WHERE email = ? AND password = SHA1(?)";
	let [row, col] = await Pool.query(sql, [email, password]);

	if (row.length == 1) {
		// Success Login

		let { id, email, name } = row[0];
		let token = jwt.sign({ id, email, name }, PrivateKey, {
			expiresIn: "7 days",
		});
		return { result: true, token, user: row[0] };
	} else {
		return { result: false, token: null, user: null };
	}
});
