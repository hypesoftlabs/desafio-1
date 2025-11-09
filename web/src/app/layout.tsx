import "./globals.css";
import { ReactNode } from "react";
import { Sidebar } from "@/components/layout/Sidebar";
import { Header } from "@/components/layout/Header";

export const metadata = {
  title: "Hypersoft Dashboard",
  description: "Painel de gestão de produtos e estoque",
};

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="pt-BR">
      <body className="flex h-screen bg-gray-50 text-gray-900">
        <Sidebar />
        <main className="flex-1 flex flex-col">
          <Header />
          <section className="p-6 overflow-y-auto">{children}</section>
        </main>
      </body>
    </html>
  );
}
