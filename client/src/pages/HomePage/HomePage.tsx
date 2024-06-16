import { useSelector } from "react-redux";

const HomePage: React.FC = () => {
  const {authData} = useSelector((state: any) => state.authReducer);
  console.log("USER HOME, ",authData);

  return <div>HomePage</div>;
};

export default HomePage;
