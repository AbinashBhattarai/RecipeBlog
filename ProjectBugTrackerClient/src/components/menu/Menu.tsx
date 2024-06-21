import { NavLink } from 'react-router-dom';
import './menu.scss'
import { GrHomeRounded, GrGroup } from "react-icons/gr";
import { FaPersonCircleCheck } from "react-icons/fa6";
import { GoProjectTemplate, GoBug } from "react-icons/go";
import { BsPersonVcard } from "react-icons/bs";
import { BiSupport } from "react-icons/bi";


const Menu = () => {
  return (
    <div className='menu'>
      <NavLink to="/" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <GrHomeRounded className='menu-icon' />
          <span>Dashboard</span>
        </div>
      </NavLink>
      <NavLink to="/client" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <GrGroup className='menu-icon' />
          <span>Client</span>
        </div>
      </NavLink>
      <NavLink to="/clientrep" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <FaPersonCircleCheck className='menu-icon' />
          <span>ClientRep</span>
        </div>
      </NavLink>
      <NavLink to="/project" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <GoProjectTemplate className='menu-icon' />
          <span>Project</span>
        </div>
      </NavLink>
      <NavLink to="/issue" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <GoBug className='menu-icon' />
          <span>Issue</span>
        </div>
      </NavLink>
      <NavLink to="/employee" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <BsPersonVcard className='menu-icon' />
          <span>Employee</span>
        </div>
      </NavLink>
      <NavLink to="/support" className={({isActive}) => `${isActive ? "navlink menuActive" : "navlink"}`}>
        <div className='menu-item'>
          <BiSupport className='menu-icon' />
          <span>Support</span>
        </div>
      </NavLink>
    </div>
  );
};

export default Menu