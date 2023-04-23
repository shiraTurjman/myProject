import { User } from "../../types/user";

const userInitial:User={
    userName: "",
    email: "",
    password: ""
};

const UserReducer=(state:User=userInitial,action:any)=>{
    switch(action.type){
        case 'SET_USER':
            state=action.data;
            state={...state}
            break;
        default:
            break;
    }
    return state;
}

export default UserReducer;