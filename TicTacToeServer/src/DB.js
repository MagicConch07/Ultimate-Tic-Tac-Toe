import mysql from "mysql2/promise";
import { DBConfig } from "./Secret.js";

export const Pool = mysql.createPool(DBConfig);
