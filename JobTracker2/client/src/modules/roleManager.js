import { getToken } from "./authManager";
const baseUrl = '/api/role';

export const getAllRoles = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }).then((resp) => {
          if (resp.ok) {
            return resp.json();
          } else {
            throw new Error(
              "An unknown error occurred while trying to get roles.",
            );
          }
        });
      });
}

export const getRoleById = (id) => {
    // return fetch(`${baseUrl}/${id}`).then((res) => res.json());
    return getToken().then((token) => {
      return fetch(`${baseUrl}/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((resp) => {
        if (resp.ok) {
          return resp.json();
        } else {
          throw new Error(
            "An unknown error occurred while trying to get role details.",
          );
        }
      });
    });
  };