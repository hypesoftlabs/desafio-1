import { createBrowserRouter } from "react-router-dom";
import Layout from "./pages/Layout";
import Dashboard from "./pages/Dashboard";
import LayoutProducts from "./pages/products/LayoutProducts";
import ListProducts from "./pages/products/ListProducts";
import CreateProduct from "./pages/products/CreateProduct";
import ShowProduct from "./pages/products/ShowProduct";
import UpdateProduct from "./pages/products/UpdateProduct";

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
        element: <LayoutProducts />,
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
    ],
  },
]);

export default router;