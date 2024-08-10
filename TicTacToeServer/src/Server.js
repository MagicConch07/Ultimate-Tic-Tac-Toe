import express from "express";

const app = express();

app.get("/test", (req, res) => {
	res.json({ msg: "Hello Unity!" });
});

app.get("/insert", (req, res) => {});

app.listen(4500, () => {
	console.log(
		`
        ##################################
        # Server is running on 4500 port #
        # http://localhost:4500          #
        ##################################`
	);
});
