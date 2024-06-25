import { IoSearch } from "react-icons/io5";
import './search.scss';

const Search = () => {
  return (
    <div className="search">
      <input type="text" />
      <IoSearch className="icon" />  
    </div>
  );
};

export default Search