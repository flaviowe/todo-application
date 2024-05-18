import express from "express";
import bodyParser from "body-parser";
import mongoose from "mongoose";
import todoRoutes from "./routes/todoRoutes";

const app = express();

app.use(bodyParser.json());

const TODO_DATABASE_URL: string = process.env.TODO_DATABASE_URL ?? "";

mongoose
  .connect(TODO_DATABASE_URL)
  .then(() => console.log("MongoDB connected"))
  .catch((err) => console.log(err));

// Rotas
app.use("/todos", todoRoutes);

export default app;
