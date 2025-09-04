import {
  LayoutDashboard,
  User,
} from "lucide-react";
import { Link, Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <div className="flex">
      <div className="navbar z-10 flex items-center justify-between w-full bg-zinc-50 gap-5 absolute top-0 p-7 mb-12">
        <h1 className="text-purple-500 text-3xl font-bold">
          HYPE{" "}
          <span className="text-zinc-600">
            STOCK
          </span>
        </h1>
        <div className="links flex gap-5 font-semibold text-zinc-700">
          <Link to="/">Overview</Link>
          <Link to="/products">Product List</Link>
          <Link to="/products/low-stock">
            Low Stock
          </Link>
          <Link to="/categories">
            Categories List
          </Link>
          <Link to="/products/new">
            Create Product
          </Link>
          <Link to="/categories/new">
            Create Category
          </Link>
        </div>

        <div className="profile flex items-center gap-2">
          <User />
          <p className="text-xl">User</p>
        </div>
      </div>
      <div className="w-60 pt-25 min-h-screen px-10 flex bg-zinc-50 align-center text-center flex-col">
        <div className="mt-5 flex justify-center">
          <Link
            to="/"
            className="flex items-center gap-2"
          >
            <LayoutDashboard />
            Dashboard
          </Link>
        </div>
      </div>
      <div className="content max-h-screen overflow-y-scroll py-25 px-10 bg-zinc-200 w-full">
        <Outlet />
      </div>
    </div>
  );
};

export default Layout;
