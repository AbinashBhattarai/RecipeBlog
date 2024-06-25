import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider, createBrowserRouter } from "react-router-dom"
import './styles/global.scss'
import Layout from './pages/layout/Layout';
import Dashboard from './pages/dashboard/Dashboard';
import Project from './pages/project/project';
import Employee from './pages/employee/Employee';



const router = createBrowserRouter([
    {
      path: '/',
      element: <Layout />,
      children: [
        {
          path: '',
          element: <Dashboard />
        },
        {
          path: '/project/',
          element: <Project />,
        },
        {
          path: '/employee/',
          element: <Employee />,
        }
      ],
    }
  ]);


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>,
);
