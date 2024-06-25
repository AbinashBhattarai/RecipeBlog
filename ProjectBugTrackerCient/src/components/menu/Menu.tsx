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
      <NavLink to="/" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <GrHomeRounded className='icon' />
          <span>Dashboard</span>
        </div>
      </NavLink>
      <NavLink to="/client" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <GrGroup className='icon' />
          <span>Client</span>
        </div>
      </NavLink>
      <NavLink to="/clientrep" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <FaPersonCircleCheck className='icon' />
          <span>ClientRep</span>
        </div>
      </NavLink>
      <NavLink to="/project" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <GoProjectTemplate className='icon' />
          <span>Project</span>
        </div>
      </NavLink>
      <NavLink to="/issue" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <GoBug className='icon' />
          <span>Issue</span>
        </div>
      </NavLink>
      <NavLink to="/employee" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <BsPersonVcard className='icon' />
          <span>Employee</span>
        </div>
      </NavLink>
      <NavLink to="/support" className={({isActive}) => `${isActive ? "active" : ""}`}>
        <div className='menu-item wrapper'>
          <BiSupport className='icon' />
          <span>Support</span>
        </div>
      </NavLink>
    </div>
  );
};

export default Menu