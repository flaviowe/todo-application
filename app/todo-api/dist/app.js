"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var _a;
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const mongoose_1 = __importDefault(require("mongoose"));
const todoRoutes_1 = __importDefault(require("./routes/todoRoutes"));
const app = (0, express_1.default)();
app.use(body_parser_1.default.json());
const TODO_DATABASE_URL = (_a = process.env.TODO_DATABASE_URL) !== null && _a !== void 0 ? _a : "";
mongoose_1.default
    .connect(TODO_DATABASE_URL)
    .then(() => console.log("MongoDB connected"))
    .catch((err) => console.log(err));
// Rotas
app.use("/todos", todoRoutes_1.default);
exports.default = app;
