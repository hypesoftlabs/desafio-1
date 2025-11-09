"use client";

import { Package, Layers, BarChart3 } from "lucide-react";
import Link from "next/link";

const menu = [
  { name: "Dashboard", icon: BarChart3, path: "/" },
  { name: "Produtos", icon: Package, path: "/produtos" },
  { name: "Categorias", icon: Layers, path: "/categorias" },
];

export function Sidebar() {
  return (
    <aside className="w-64 bg-white border-r flex flex-col p-4">
      <h1 className="text-2xl font-bold mb-6">Hypersoft</h1>
      <nav className="flex flex-col gap-2">
        {menu.map((item) => (
          <Link
            key={item.path}
            href={item.path}
            className="flex items-center gap-3 px-3 py-2 rounded-lg hover:bg-gray-100"
          >
            <item.icon className="w-5 h-5 text-gray-600" />
            <span>{item.name}</span>
          </Link>
        ))}
      </nav>
    </aside>
  );
}
