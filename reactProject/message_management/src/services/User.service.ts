
import axios from "axios"
import { User } from "../types/user"

class UserService {
    async getAllUserList() {
        
    let listUser =await axios.get('https://jsonplaceholder.typicode.com/users');
     let allUsers:User  [] =listUser.data;  
     return allUsers;
     }
}


export default new UserService