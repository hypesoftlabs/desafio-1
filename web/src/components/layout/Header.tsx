"use client";

export function Header() {
  return (
    <header className="w-full h-16 flex items-center justify-between border-b bg-white px-6">
      <h2 className="text-lg font-semibold">Painel de Gestão</h2>
      <div className="flex items-center gap-4">
        <span className="text-sm text-gray-600">Olá, Eduardo 👋</span>
      </div>
    </header>
  );
}
