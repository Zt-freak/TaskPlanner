import React from 'react';
import Task from './task';

class Column extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: '',
            tasks: []
        };

        this.titleChange = this.titleChange.bind(this);
        this.titleBlur = this.titleBlur.bind(this);
        this.addTask = this.addTask.bind(this);
        this.delete = this.delete.bind(this);
    }

    async componentDidMount() {
        const response = await fetch('/api/column/get?id=' + this.props.columnId);
        const json = await response.json();
        this.setState({
            title: json.Title,
            tasks: json.Tasks
        });
    }

    titleChange(event) {
        this.setState({ title: event.target.value });
    }

    async titleBlur() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                boardColumnId: this.props.columnId,
                Title: this.state.title
            })
        };
        const response = await fetch('/api/column/edit', requestOptions);
        const json = await response.json();
        this.setState({
            title: json.Title,
        });
    }

    async delete() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                boardColumnId: this.props.columnId,
            })
        };
        const response = await fetch('/api/column/delete', requestOptions);
        const json = await response.json();
        this.props.unmountMe(this.props.columnId);
    }

    async addTask() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                boardColumnId: this.props.columnId,
                Title: 'New Task'
            })
        };
        const response = await fetch('/api/task/add', requestOptions);
        const json = await response.json();
        this.setState(prevState => ({
            tasks: [...prevState.tasks, json]
        }));
    }

    render() {
        return <div className="column">
            <div className="column__options">
                <input className="column__title" type="text" value={this.state.title} onChange={this.titleChange} onBlur={this.titleBlur} />
                <button onClick={this.delete} >Delete</button>
            </div>
            <div className="column__tasks" >
                {
                   this.state.tasks.map((item) =>
                       (<Task title={item.Title} content={item.Content} taskId={item.TaskId} />)
                   )
                }
                <div className="task--add" onClick={this.addTask}></div>
            </div>
        </div>;
    }
}

export default Column;