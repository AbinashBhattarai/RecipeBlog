import { ChangeEvent, FormEvent, useState } from "react";
import { IClient } from "../../interface/global.interface";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Form from '../../components/form/Form'


const AddClient = () => {
  const[formData, setformData] = useState<Partial<IClient>>({code:"", details:""})
  const[error, setError] = useState({});
  const redirect = useNavigate();

  const handleTextChange = (event : ChangeEvent<HTMLInputElement>) => {
    setformData({...formData, [event.target.name]: event.target.value});
  };

  const handleFormSubmit = (event : FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data: Partial<IClient> = {
      code: formData.code,
      details: formData.details
    };
  
    axios.post("https://localhost:7252/api/Company", data)
      .then(response => redirect("/client"))
      .catch((error) => console.log(error))
  }

    return (
      <Form 
        handleSubmit={handleFormSubmit} 
        handleChange={handleTextChange} 
        value={formData}
        name="add"
        disabled={false}
      />
    );
};

export default AddClient