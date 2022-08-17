import { User } from "../_models/user";

export const getUser = () : User | null => {
    let response = localStorage.getItem("currentUser");
    if(response) {
        let data = JSON.parse(response);
        let user = new User();
        user.email = data.email;
        user.firstName = data.firstName;
        user.lastName = data.lastName;
        user.middleName = data.middleName;
        user.username = data.username;
        user.token = data.token;
        return user; 
    }
    return null;
}

export const setUser = (user: string) => {
    localStorage.setItem("currentUser", user)
}

export const removeUser = () => {
    localStorage.removeItem("currentUser");
}