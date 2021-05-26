import React from 'react';
import ReactDOM from 'react-dom';

import Board from './board';

const boardId = document.getElementById("root").dataset.id;

const App = () => <Board boardId={boardId} name="board"/>;

ReactDOM.render(<App />, document.getElementById("root"));