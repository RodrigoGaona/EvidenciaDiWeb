import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';

function App() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5163/api/movies") // Ajusta el puerto si es diferente
      .then(response => response.json())
      .then(data => setMovies(data))
      .catch(error => console.error("Error fetching movies:", error));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>

        <div>
          <h2>Lista de Películas</h2>
          <ul>
            {movies.map((movie) => (
              <li key={movie.id}>{movie.title}</li>
            ))}
          </ul>
        </div>
      </header>
    </div>
  );
}

export default App;