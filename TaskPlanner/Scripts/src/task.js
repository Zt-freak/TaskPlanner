import React from 'react';

class Task extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: this.props.title,
            content: this.props.content,
            moveValue: this.props.columnId
        };

        this.titleChange = this.titleChange.bind(this);
        this.titleBlur = this.titleBlur.bind(this);
        this.contentChange = this.contentChange.bind(this);
        this.contentBlur = this.contentBlur.bind(this);
        this.delete = this.delete.bind(this);
        this.move = this.move.bind(this);
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

    async move(event) {
        await this.setState({ moveValue: event.target.value });

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                taskId: this.props.taskId,
                boardColumnId: this.state.moveValue
            })
        };
        const response = await fetch('/api/task/edit', requestOptions);
        const json = await response.json();
        this.props.unmountMe(this.props.taskId, this.state.moveValue);
    }

    titleChange(event) {
        this.setState({ title: event.target.value });
    }

    async titleBlur() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                taskId: this.props.taskId,
                Title: this.state.title
            })
        };
        const response = await fetch('/api/task/edit', requestOptions);
        const json = await response.json();
        this.setState({
            title: json.Title,
        });
    }

    contentChange(event) {
        this.setState({ content: event.target.value });
    }

    async contentBlur() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                taskId: this.props.taskId,
                Content: this.state.content
            })
        };
        const response = await fetch('/api/task/edit', requestOptions);
        const json = await response.json();
        this.setState({
            content: json.Content,
        });
    }

    render() {
        let content = ""
        if (this.state.content) {
            content = this.state.content
        }

        return <div className="task">
            <input type="text" className="task__title" value={this.state.title} onChange={this.titleChange} onBlur={this.titleBlur}></input>
            <textarea className="task__content" value={this.state.content} onChange={this.contentChange} onBlur={this.contentBlur}></textarea>
            <button onClick={this.delete}>Delete</button>
            <label>Move to:</label>
            <select onChange={this.move} value={this.state.moveValue}>
                {
                    this.props.columns.map((item) =>
                        (<option value={item.BoardColumnId} key={item.BoardColumnId}>{item.Title}</option>)
                    )
                }
            </select>
        </div>;
    }
}

export default Task;