import './navbar.scss'
import Search from '../search/Search'
import { GrChatOption, GrNotification, GrDown } from "react-icons/gr";
import { NavLink } from 'react-router-dom';

const Navbar = () => {
  return (
    <div className='navbar'>
      <NavLink to='/' className='logo wrapper'>
        <img src="/logo.svg" alt="logo" />
        <div className='logo-text'>
          <span>Bug</span>
          <span>Tracker</span>
        </div>
      </NavLink>

      <div className='navbar-content wrapper'>
        <Search />

        <div className='icons'>
          <div className='icon-container'>
            <GrNotification className='icon'/>
            <span className='count'>2</span>
          </div>

          <div className='icon-container'>
            <GrChatOption className='icon'/>
            <span className='count'>1</span>
          </div>

          <div className='user-icon'>
            <img src="/avatar.jpg" alt="user pic"/>
            <span>Robby</span>
            <div className='icon-container'>
              <GrDown className='icon'/>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Navbar