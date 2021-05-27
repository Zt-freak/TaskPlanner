import React from 'react';
import Column from './column';

class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: '',
            columns: []
        };


        this.titleChange = this.titleChange.bind(this);
        this.titleBlur = this.titleBlur.bind(this);
        this.addColumn = this.addColumn.bind(this);
        this.handleColumnUnmount = this.handleColumnUnmount.bind(this);
        this.transferTask = this.transferTask.bind(this);
    }

    async componentDidMount() {
        const response = await fetch('/api/board/get?id=' + this.props.boardId);
        const json = await response.json();
        this.setState({
            title: json.Title,
            columns: json.BoardColumns
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
                boardId: this.props.boardId,
                Title: this.state.title
            })
        };
        const response = await fetch('/api/board/edit', requestOptions);
        const json = await response.json();
        this.setState({
            title: json.Title,
        });
    }

    async addColumn() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                boardId: this.props.boardId,
                Title: 'New Column'
            })
        };
        const response = await fetch('/api/column/add', requestOptions);
        const json = await response.json();
        this.setState(prevState => ({
            columns: [...prevState.columns, json]
        }));
    }

    handleColumnUnmount(columnId) {
        const tempColumns = [...this.state.columns];
        for (let i = 0; i < tempColumns.length; i++) {
            if (tempColumns[i].BoardColumnId == columnId) {
                tempColumns.splice(i, 1);
            }
        }

        this.setState({ columns: tempColumns });
    }

    transferTask(task, moveColummId) {
        const tempColumns = [...this.state.columns]
        for (let i = 0; i < tempColumns.length; i++) {
            if (tempColumns[i].BoardColumnId == moveColummId) {
                tempColumns[i].Tasks.push(task);
            }
        }

        this.setState({ columns: tempColumns });
    }

    render() {
        return <div className="board">
            <div className="board__options">
                <input className="board__title" type="text" value={this.state.title} onChange={this.titleChange} onBlur={this.titleBlur} />
            </div>
            <div className="board__columns">
                {
                    this.state.columns.map((item) =>
                        (<Column title={item.Title} columnId={item.BoardColumnId} key={'column' + item.BoardColumnId} unmountMe={this.handleColumnUnmount} transferTask={this.transferTask} Tasks={item.Tasks} columns={this.state.columns}/>)
                    )
                }
                <div className="column--add" onClick={this.addColumn}></div>
            </div>
        </div>;
    }
}

export default Board;