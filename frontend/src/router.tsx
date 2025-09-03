import { createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout/Layout";
import Dashboard from "./pages/Dashboard";
import ListProducts from "./pages/products/ListProducts";
import CreateProduct from "./pages/products/CreateProduct";
import ShowProduct from "./pages/products/ShowProduct";
import UpdateProduct from "./pages/products/UpdateProduct";
import ListCategories from "./pages/categories/ListCategories";
import CreateCategory from "./pages/categories/CreateCategory";
import UpdateCategory from "./pages/categories/UpdateCategory";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      { 
        index: true, 
        element: <Dashboard /> 
      },
      {
        path: "products",
        children: [
          { index: true, element: <ListProducts /> },
          {
            path: "new",
            element: <CreateProduct />,
          },
          { 
            path: ":id", 
            element: <ShowProduct /> 
          },
          {
            path: ":id/update",
            element: <UpdateProduct />,
          },
        ],
      },
      {
        path: "categories",
        children: [
          { index: true, element: <ListCategories /> },
          {
            path: "new",
            element: <CreateCategory />,
          },
          {
            path: ":id/update",
            element: <UpdateCategory />,
          },
        ],
      },
    ],
  },
]);

export default router;