import { Outlet, RouterProvider, createBrowserRouter } from "react-router-dom"
import Dashboard from "./pages/dashboard/Dashboard";
import Company from "./pages/company/Company";
import Customer from "./pages/customer/Customer";
import Employee from "./pages/employee/Employee";
import Project from "./pages/project/Project";
import Navbar from "./components/navbar/Navbar";
import Menu from "./components/menu/Menu";
import Footer from "./components/footer/Footer";
import './styles/global.scss'

function App() {

  const Layout = () => {
    return(
      <div className="main">
        <Navbar />

        <div className="container">
          <div className="menu-container">
            <Menu />
          </div>
          <div className="content-container">
            <Outlet />
          </div>
        </div>

        <Footer />
      </div>
    );
  };

  const router = createBrowserRouter([
    {
      path: '/',
      element: <Layout />,
      children: [
        {
          path: '/',
          element: <Dashboard />
        },
        {
          path: '/company',
          element: <Company />
        },
        {
          path: '/customer',
          element: <Customer />
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

  return (<RouterProvider router={router}/>);
}

export default App
