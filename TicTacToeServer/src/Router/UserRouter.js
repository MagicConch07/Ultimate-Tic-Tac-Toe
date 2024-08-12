import { Router } from "express";
import { Pool } from "../DB.js";
import { MsgType, MSG } from "./CommonPacket.js";
import { PrivateKey } from "../Secret.js";
import jwt from "jsonwebtoken";

export const UserRouter = new Router();

UserRouter.post("/user_register", async (req, res) => {
	let { email, password, name } = req.body;

	const sql =
		"INSERT INTO users (email, password, name) VALUES (?, SHA1(?), ?)";
	try {
		let result = await Pool.query(sql, [email, password, name]);

		MSG.type = MsgType.SUCESS;
		MSG.msg = "가입 완료";
		res.json(MSG);
	} catch (err) {
		MSG.type = MsgType.ERROR;
		MSG.msg = "서버 오류 발생";
		//TODO : 이거는 나중에 에러 코드 걸러줘도 좋음 근데 귀찮은 작업 일듯
		res.json(MSG);
	}
});

UserRouter.post("/user_login", async (req, res) => {
	let { email, password } = req.body;

	let { result, token } = await LoginProcess(email, password);

	if (result) {
		MSG.type = MsgType.SUCCESS;
		MSG.msg = token;
		res.json(MSG);
	} else {
		MSG.type = MsgType.ERROR;
		MSG.msg = "입력한 값이 올바르지 않습니다.";
		res.json(MSG);
	}
});

async function LoginProcess(email, password) {
	const sql = "SELECT * FROM users WHERE email = ? AND password = SHA1(?)";
	let [row, col] = await Pool.query(sql, [email, password]);

	if (row.length == 1) {
		//Success Login
		let { id, email, name } = row[0];
		let token = jwt.sign({ id, email, name }, PrivateKey, {
			expiresIn: "7 days",
		});
		return { result: true, token, user: row[0] };
	} else {
		return { result: false, token: null, user: null };
	}
}

UserRouter.get("/verify_token", (req, res) => {
	let token = extractToken(req);

	try {
		let decodedToken = jwt.verify(token, PrivateKey);
		if (decodedToken != null) {
			MSG.type = MsgType.SUCCESS;
		} else {
			MSG.type = MsgType.ERROR;
		}
	} catch (err) {
		console.log(err);
		MSG.type = MsgType.ERROR;
	}
	res.json(MSG);
});

function extractToken(req) {
	let prefix = "Bearer";
	let auth = req.headers.authorization;
	let token = auth.includes(prefix) ? auth.split(prefix)[1] : auth;

	return token;
}
