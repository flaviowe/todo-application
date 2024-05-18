"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.deleteTodo = exports.updateTodo = exports.getTodoById = exports.createTodo = exports.getTodos = void 0;
const todo_1 = __importDefault(require("../models/todo"));
const getTodos = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const todos = yield todo_1.default.find();
        res.status(200).json(todos);
    }
    catch (err) {
        res.status(500).send(err);
    }
});
exports.getTodos = getTodos;
const createTodo = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const newTodo = new todo_1.default(req.body);
        const savedTodo = yield newTodo.save();
        res.status(201).json(savedTodo);
    }
    catch (err) {
        res.status(500).send(err);
    }
});
exports.createTodo = createTodo;
const getTodoById = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const todo = yield todo_1.default.findById(req.params.id);
        if (todo) {
            res.status(200).json(todo);
        }
        else {
            res.status(404).send('Todo not found');
        }
    }
    catch (err) {
        res.status(500).send(err);
    }
});
exports.getTodoById = getTodoById;
const updateTodo = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const updatedTodo = yield todo_1.default.findByIdAndUpdate(req.params.id, req.body, { new: true });
        if (updatedTodo) {
            res.status(200).json(updatedTodo);
        }
        else {
            res.status(404).send('Todo not found');
        }
    }
    catch (err) {
        res.status(500).send(err);
    }
});
exports.updateTodo = updateTodo;
const deleteTodo = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const deletedTodo = yield todo_1.default.findByIdAndDelete(req.params.id);
        if (deletedTodo) {
            res.status(200).json(deletedTodo);
        }
        else {
            res.status(404).send('Todo not found');
        }
    }
    catch (err) {
        res.status(500).send(err);
    }
});
exports.deleteTodo = deleteTodo;
