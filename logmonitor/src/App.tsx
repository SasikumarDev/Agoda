import React from 'react';
import './App.css';
import { Route, BrowserRouter, Outlet, Routes } from 'react-router-dom';
import Navbar from './Components/Navbar';
import Home from './Pages/Home';
import Logs from './Pages/Logs';
import { GlobalContextProvider } from './Context/LogMonitorContext';
import Login from './Pages/Login';
import ViewLogs from './Pages/ViewLogs';

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
            <Route path='ViewLog'>
              <Route path=':id' element={<ViewLogs />} />
            </Route>
          </Routes>
          <Outlet />
        </div>
      </BrowserRouter>
    </GlobalContextProvider>
  );
}

export default App;
