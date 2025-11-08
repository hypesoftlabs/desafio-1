import keycloak from "./keycloak";

export const initKeycloak = (): Promise<string> => {
  return new Promise((resolve, reject) => {
    keycloak
      .init({ onLoad: "login-required" }) // força login
      .then((authenticated) => {
        if (authenticated) {
          resolve(keycloak.token!);
        } else {
          reject("Não autenticado");
        }
      })
      .catch((err) => reject(err));
  });
};
