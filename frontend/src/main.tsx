import './index.css'
import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { Layout } from './components/layout';
import { Products } from './pages/Products';
import { Categories } from './pages/Categories';
import { Stock } from './pages/Stock';
import { Dashboard } from './pages/Dashboard';


const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout/>, 
    children: [
      {
        path: "/dashboard", 
        element: <Dashboard />,
      },
      {
        path: "/produtos", 
        element: <Products />,
      },
      {
        path: "/categorias", 
        element: <Categories />,
      },
      {
        path: "/estoque", 
        element: <Stock />,
      },
    ]
  },
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);