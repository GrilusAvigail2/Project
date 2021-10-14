import React from 'react';
import logo from '../../logo.svg';
import './App.css';

function aaa(f:string){
  alert("click");
  const a="2";
  let x= a+1;
  let b=5;
  let c:string="3";
  let array=[1,2,3,4,5];
  array.push(1);
  array.push(4);
  console.log(array);
}

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
      <button className="btnAlert" onClick={()=>aaa("a")}>click me</button>
    </div>
  );
}

export default App;
