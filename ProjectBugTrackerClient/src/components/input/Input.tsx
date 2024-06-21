import './input.scss'
import { ChangeEvent, useState } from 'react';
import { GrSearch } from "react-icons/gr";


const Input = () => {
  const[searchText, setSearchText] = useState("");

  const handleText = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchText(e.target.value);
  };

  const handleSearch = () => {
    setSearchText("");
  }

  return (
    <div className="search-input">
      <input
        type="text" 
        placeholder="search"
        value={searchText} 
        onChange={(e) => handleText(e)} 
      />
      <div onClick={handleSearch}
        className='icon-container'
      >
        <GrSearch className='icon'/>
      </div>
    </div>
  );
};

export default Input