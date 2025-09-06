import {
  LayoutDashboard,
  PackageSearch,
  AlertTriangle,
  ListTree,
  PackagePlus,
  FolderPlus,
  User,
} from "lucide-react";
import { Link, Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <div className="flex">
      <div className="navbar z-10 flex items-center justify-between w-full bg-zinc-50 gap-5 absolute top-0 p-7 mb-12">
        <Link to="/" className="text-purple-700 text-3xl font-bold">
          HYPE{" "}
          <span className="text-zinc-600">
            STOCK
          </span>
        </Link>
        <div className="links flex gap-5 font-semibold text-zinc-700">
          <Link
            to="/"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors"
          >
            <LayoutDashboard size={18} /> Overview
          </Link>
          <Link
            to="/products"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors"
          >
            <PackageSearch size={18} />
            Product List
          </Link>
          <Link
            to="/categories"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors"
          >
            <ListTree size={18} />
            Categories List
          </Link>
          <Link
            to="/products/new"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors"
          >
            <PackagePlus size={18} />
            Create Product
          </Link>
          <Link
            to="/categories/new"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors"
          >
            <FolderPlus size={18} />
            Create Category
          </Link>
        </div>

        <div className="profile flex items-center gap-2">
          <User />
          <p className="text-xl">User</p>
        </div>
      </div>
      <div className="w-60 pt-25 min-h-screen flex bg-zinc-50 align-center text-center flex-col">
        <div className="mt-10 items-center flex justify-center flex-col gap-10">
          <Link
            to="/"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors font-semibold"
          >
            <LayoutDashboard />
            Dashboard
          </Link>
          <Link
            to="/products/low-stock"
            className="flex items-center gap-2 text-gray-700 dark:text-gray-300 hover:text-purple-700 hover:underline transition-colors font-semibold"
          >
            <AlertTriangle />
            Low Stock
          </Link>
        </div>
      </div>
      <div className="content max-h-screen overflow-y-scroll py-30 px-10 bg-zinc-200 w-full">
        <Outlet />
      </div>
    </div>
  );
};

export default Layout;
