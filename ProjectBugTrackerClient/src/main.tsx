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
import Support from './pages/support/Support';
import Issue from './pages/Issue/Issue';
import AddClient from './pages/addClient/AddClient';
import EditClient from './pages/editClient/EditClient';


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
          path: '/client/',
          element: <Client />,
        },
        {
          path: '/client/add',
          element: <AddClient />
        },
        {
          path: '/client/edit/:id',
          element: <EditClient />
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
        },
        {
          path: '/issue',
          element: <Issue />
        },
        {
          path: '/support',
          element: <Support />
        }
      ],
    }
  ]);


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>,
)
