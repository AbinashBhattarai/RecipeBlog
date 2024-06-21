import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { IClient } from "../../interface/global.interface";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
import Form from '../../components/form/Form'


const EditClient = () => {
  const[formData, setformData] = useState<Partial<IClient>>({code:"", details:""})
  const redirect = useNavigate();

  const {id} = useParams();

  useEffect(() => {
    axios.get<IClient>(`https://localhost:7252/api/Company/${id}`)
      .then(response => setformData({
        code: response.data.code,
        details: response.data.details
      }));
  }, []);

  const handleTextChange = (event : ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.value)
    setformData({...formData, [event.target.name]: event.target.value});
  };

  const handleFormSubmit = (event : FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data: Partial<IClient> = {
      details: formData.details
    };
      
    axios.put(`https://localhost:7252/api/Company/${id}`, data)
      .then(response => redirect("/client"))
      .catch((error) => console.log(error))
  }

    return (
      <div>
        <Form 
          handleSubmit={handleFormSubmit} 
          handleChange={handleTextChange} 
          value={formData} 
          name="Update"
          disabled={true}
        />
        <button onClick={() => redirect("/client")}>Back</button>
      </div>
    );
};

export default EditClient


  