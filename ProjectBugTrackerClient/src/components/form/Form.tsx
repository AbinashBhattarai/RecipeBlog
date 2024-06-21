import { ChangeEvent, FormEvent } from "react"
import { IClient } from "../../interface/global.interface"

interface IForm {
  handleSubmit(event: FormEvent<HTMLFormElement>): void,
  handleChange(event: ChangeEvent<HTMLInputElement>): void,
  value: Partial<IClient>,
  name: string,
  disabled: boolean
};

const form: React.FC<IForm> = ({handleSubmit, handleChange, value, name, disabled}) => {
  return (
    <form onSubmit={handleSubmit}>
      <label htmlFor="code">Code</label>
      <input 
        type="text" id="code" name="code"
        defaultValue={value.code}
        onChange={handleChange}
        disabled={disabled}
      />
      <label htmlFor="details">Details</label>
      <input 
        type="text" id="details" name="details" 
        defaultValue={value.details}
        onChange={handleChange}
      />
      <button type="submit">{name}</button>
    </form>
  )
}

export default form