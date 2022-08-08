import React from 'react';
import './App.css';
import { Route, BrowserRouter, Outlet, Routes } from 'react-router-dom';
import Navbar from './Components/Navbar';
import Home from './Pages/Home';
import Logs from './Pages/Logs';
import { GlobalContextProvider } from './Context/LogMonitorContext';
import Login from './Pages/Login';

function App() {
  return (
    <GlobalContextProvider>
      <BrowserRouter>
      <Navbar />
        <div style={{ margin: '0.6rem' }} >
          <Routes>
            <Route path='/' element={<Login />} />
            <Route path='Logs' element={<Logs />} />
            <Route path='Home' element={<Home />} />
          </Routes>
          <Outlet />
        </div>
      </BrowserRouter>
    </GlobalContextProvider>
  );
}

export default App;
