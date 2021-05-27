import React from 'react';

class Task extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: '',
            content: ''
        };

        this.delete = this.delete.bind(this);
    }

    async delete() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                taskId: this.props.taskId,
            })
        };
        const response = await fetch('/api/task/delete', requestOptions);
        const json = await response.json();
        this.props.unmountMe(this.props.taskId);
    }

    render() {
        return <div className="task">
            <div className="task__title">{this.props.title}</div>
            <div className="task__content">{this.props.content}</div>
            <button onClick={this.delete}>Delete</button>
        </div>;
    }
}

export default Task;