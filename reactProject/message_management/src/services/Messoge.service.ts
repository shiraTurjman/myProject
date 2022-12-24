
import axios from "axios"
import { Message } from "../types/message";
class messageService {
    async getAllMassageList() {
        
        let listMassge =await axios.get('https://jsonplaceholder.typicode.com/posts');
         let allMassage:Message  [] =listMassge.data;  
         return allMassage;
         }
}


export default new messageService