import { Request, Response } from 'express';
import Todo, { ITodo } from '../models/todo';

export const getTodos = async (req: Request, res: Response): Promise<void> => {
    try {
        const todos = await Todo.find();
        res.status(200).json(todos);
    } catch (err) {
        res.status(500).send(err);
    }
};

export const createTodo = async (req: Request, res: Response): Promise<void> => {
    try {
        const newTodo: ITodo = new Todo(req.body);
        const savedTodo = await newTodo.save();
        res.status(201).json(savedTodo);
    } catch (err) {
        res.status(500).send(err);
    }
};

export const getTodoById = async (req: Request, res: Response): Promise<void> => {
    try {
        const todo = await Todo.findById(req.params.id);
        if (todo) {
            res.status(200).json(todo);
        } else {
            res.status(404).send('Todo not found');
        }
    } catch (err) {
        res.status(500).send(err);
    }
};

export const updateTodo = async (req: Request, res: Response): Promise<void> => {
    try {
        const updatedTodo = await Todo.findByIdAndUpdate(req.params.id, req.body, { new: true });
        if (updatedTodo) {
            res.status(200).json(updatedTodo);
        } else {
            res.status(404).send('Todo not found');
        }
    } catch (err) {
        res.status(500).send(err);
    }
};

export const deleteTodo = async (req: Request, res: Response): Promise<void> => {
    try {
        const deletedTodo = await Todo.findByIdAndDelete(req.params.id);
        if (deletedTodo) {
            res.status(200).json(deletedTodo);
        } else {
            res.status(404).send('Todo not found');
        }
    } catch (err) {
        res.status(500).send(err);
    }
};
