import jwt from "jsonwebtoken";
import { PrivateKey } from "../Secret.js";

export const LoginChecker = (req, res, next) => {
	let token = extractToken(req);
	req.user = null;

	if (token != undefined) {
		try {
			let decodedToken = jwt.verify(token, PrivateKey);

			let { id, email, name } = decodedToken;
			req.user = { id, email, name };
		} catch (err) {
			console.log(err);
			console.log("Login되지 않은 유져 들어옴");
		}
	}
	next();
};

function extractToken(req) {
	let prefix = "Bearer";
	let auth = req.headers.authorization;
	if (auth == undefined) return auth;

	let token = auth.includes(prefix) ? auth.split(prefix)[1] : auth;

	return token;
}
