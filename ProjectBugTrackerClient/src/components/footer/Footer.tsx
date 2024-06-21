import './footer.scss'

const Footer = () => {
  return (
    <div className='footer wrapper'>
      <div className='user-icon'>
        <img src="/avatar.jpg" alt="user pic"/>
        <span>Robby</span>
      </div>

      <div className='copyright'>
        @Copyright2024
      </div>
    </div>
  );
};

export default Footer