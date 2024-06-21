import Navbar from "../../components/navbar/Navbar";
import Menu from "../../components/menu/Menu";
import Footer from "../../components/footer/Footer";
import { Outlet } from "react-router-dom"
import './layout.scss'


const Layout = () => {
  return (
    <div className="main">
      <Navbar />

      <div className="container">
        <div className="menu-container">
          <Menu />
        </div>
        <div className="content-container wrapper">
          <Outlet />
        </div>
      </div>

      <Footer />
    </div>
  )
}

export default Layout