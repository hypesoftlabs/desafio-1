import { Outlet } from "react-router-dom";

const LayoutProducts = () => {
  return (
    <div>
      <h1>Layout products</h1>
      <Outlet />
    </div>
  );
};

export default LayoutProducts;
