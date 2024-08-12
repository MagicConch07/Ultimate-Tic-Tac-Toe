import express from "express";
import { UserRouter } from "./Router/UserRouter.js";
import { LoginChecker } from "./Middleware/LoginChecker.js";

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.use(LoginChecker);

app.use(UserRouter);

app.listen(4500, () => {
	console.log(
		`
        ##################################
        # Server is running on 4500 port #
        # http://localhost:4500          #
        ##################################`
	);
});
