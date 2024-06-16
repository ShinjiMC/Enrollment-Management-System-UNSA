import { useSelector } from "react-redux";

const HomePage: React.FC = () => {
  const { user } = useSelector((state: any) => state.authReducer.authData);
  console.log(user);

  return <div>HomePage</div>;
};

export default HomePage;
