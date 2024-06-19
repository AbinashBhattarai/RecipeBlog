import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider, createBrowserRouter } from "react-router-dom"
import Dashboard from "./pages/dashboard/Dashboard";
import Client from "./pages/client/Client";
import ClientRep from "./pages/clientRep/ClientRep";
import Employee from "./pages/employee/Employee";
import Project from "./pages/project/Project";
import Layout from './pages/layout/Layout';
import './styles/global.scss';


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
          path: '/client',
          element: <Client />
        },
        {
          path: '/clientRep',
          element: <ClientRep />
        },
        {
          path: '/employee',
          element: <Employee />
        },
        {
          path: '/project',
          element: <Project />
        }
      ]
    }
  ]);


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>,
)
