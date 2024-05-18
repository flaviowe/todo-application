import { Schema, model, Document } from 'mongoose';

interface ITodo extends Document {
    title: string;
    description: string;
    completed: boolean;
}

const todoSchema = new Schema<ITodo>({
    title: { type: String, required: true },
    description: { type: String, required: true },
    completed: { type: Boolean, default: false }
});

const Todo = model<ITodo>('Todo', todoSchema);

export default Todo;
export { ITodo };
