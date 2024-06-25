import Navbar from '../../components/navbar/Navbar'
import Menu from '../../components/menu/Menu'
import { Outlet } from 'react-router-dom'
import './layout.scss';

const Layout = () => {
  return (
    <div className='main'>
      <Navbar />

      <div className='container'>
        <div className='menu-container'>
          <Menu />
        </div>
        <div className='content-container'>
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default Layout