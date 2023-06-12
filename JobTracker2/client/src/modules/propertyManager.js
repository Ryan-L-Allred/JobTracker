import { getToken } from "./authManager";
const expLevelUrl = '/api/property/explevel';
const jobTypeUrl = '/api/property/jobtype';
const jobSiteUrl = '/api/property/jobsite';

export const getAllExpLevels = () => {
    return getToken().then((token) => {
        return fetch(expLevelUrl, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }).then((resp) => {
          if (resp.ok) {
            return resp.json();
          } else {
            throw new Error(
              "An unknown error occurred while trying to get experience levels.",
            );
          }
        });
      });
};

export const getAllJobTypes = () => {
    return getToken().then((token) => {
        return fetch(jobTypeUrl, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }).then((resp) => {
          if (resp.ok) {
            return resp.json();
          } else {
            throw new Error(
              "An unknown error occurred while trying to get job types.",
            );
          }
        });
      });
};

export const getAllJobSites = () => {
    return getToken().then((token) => {
        return fetch(jobSiteUrl, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }).then((resp) => {
          if (resp.ok) {
            return resp.json();
          } else {
            throw new Error(
              "An unknown error occurred while trying to get job sites.",
            );
          }
        });
      });
};