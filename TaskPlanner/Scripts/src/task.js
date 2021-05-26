import React from 'react';

class Task extends React.Component {
    render() {
        return <div className="task">
            <div className="task__title">{this.props.title}</div>
            <div className="task__content">{this.props.content}</div>
            <button>Delete</button>
        </div>;
    }
}

export default Task;