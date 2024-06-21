import { useEffect, useState } from 'react';
import './client.scss';
import { IClient } from '../../interface/global.interface';
import axios from 'axios';
import { NavLink, Outlet, useNavigate, } from 'react-router-dom';


const Client = () => {
  const[clients, setClients] = useState<IClient[]>([]);
  const redirect = useNavigate();


  const GetAllClients = async() => {
    try{
      const response = await axios.get<IClient[]>("https://localhost:7252/api/Company");
      setClients(response.data);
    }
    catch(error){
      console.log(error);
    }
  };

  const DeleteClient = async(id: number) => {
    try{
      await axios.delete(`https://localhost:7252/api/Company/${id}`)
    }
    catch(error){
      console.log(error);
    }
  };

  useEffect(() => {
    GetAllClients();
  }, [DeleteClient]);

  return (
    <>
      <div className='client'>
        <div className='title'>
          Client
        </div>
        <NavLink to="/client/add">Add Client</NavLink>
        <table>
          <thead>
            <tr>
              <td>Code</td>
              <td>Details</td>
            </tr>
          </thead>
          <tbody>
            {clients.map(client => (
              <tr key={client.id}>
                <td>{client.code}</td>
                <td>{client.details}</td>
                <button onClick={() => redirect(`/client/edit/${client.id}`)}>Edit</button>
                <button onClick={() => DeleteClient(client.id)}>Delete</button>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <Outlet />
    </>
  );
};

export default Client