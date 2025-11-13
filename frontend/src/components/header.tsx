import { LogOut, Bell } from "lucide-react";
import logo from "@/assets/logo.png"
import { useNavigate } from "react-router-dom";

import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip";
import { useDashboardSummary } from "@/hooks/dashboard/useSummary";
import { useAuth } from "@/stores/AuthProvider";

function getInitials(name: string): string {
  if (!name) return "";
  const parts = name.trim().split(" ").filter(Boolean);
  if (parts.length === 1) {
    return parts[0].slice(0, 2).toUpperCase();
  }
  return (parts[0][0] + parts[parts.length - 1][0]).toUpperCase();
}

export const Header = () => {
  const { data: summary } = useDashboardSummary();
  const lowStockCount = summary?.lowStorageProducts.length ?? 0;

  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const displayName = user?.name ?? user?.username ?? "Usuário";
  const role = user?.role ?? "Usuário";
  const initials = getInitials(displayName) || "??";

  function handleNotificationsClick() {
    navigate("/estoque");
  }

  function handleLogoutClick() {
    logout();
    navigate("/login");
  }

  return (
    <header className="flex h-20 items-center text-center">
      <div className="flex w-64 items-center">
        <img src={logo} alt="logo" className="h-20" />
        <h1 className="text-2xl font-bold">Shop</h1>
        <h1 className="text-2xl font-bold text-gray-600">Manager</h1>
      </div>

      <div className="ml-auto flex items-center gap-6 p-2">
        {/* Botão de notificações com tooltip */}
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger asChild>
              <button
                type="button"
                onClick={handleNotificationsClick}
                className="relative flex h-10 w-10 items-center justify-center rounded-full bg-gray-100 text-slate-700 hover:bg-gray-200"
              >
                <Bell className="h-5 w-5" />
                {lowStockCount > 0 && (
                  <span className="absolute -right-1 -top-1 flex min-h-[18px] min-w-[18px] items-center justify-center rounded-full bg-red-500 px-1 text-[10px] font-semibold text-white">
                    {lowStockCount}
                  </span>
                )}
              </button>
            </TooltipTrigger>
            <TooltipContent>
              <p className="text-sm">Ver itens com baixo estoque</p>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>

        {/* Avatar com iniciais */}
        <span className="grid h-12 w-12 place-content-center rounded-full bg-emerald-500 text-lg font-semibold text-white">
          {initials}
        </span>

        {/* Nome + role */}
        <div className="flex flex-col items-start">
          <h2 className="text-base font-semibold">{displayName}</h2>
          <h2 className="text-sm text-gray-500">{role}</h2>
        </div>

        {/* Logout */}
        <button
          type="button"
          onClick={handleLogoutClick}
          className="text-red-600 hover:text-red-700"
        >
          <LogOut className="h-5 w-5 cursor-pointer" />
        </button>
      </div>
    </header>
  );
};
