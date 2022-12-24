import { forwardRef, useEffect, useImperativeHandle, useState } from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { storeType } from "../../redux/reducers/rootReducer";
import { Message } from "../../types/message";
//import userService from "../../services/User.service";
import messageService from "../../services/Messoge.service"
import { Table } from "react-bootstrap";

const MessageCom =forwardRef((props, ref)=> {

    const User = useSelector((store:storeType)=>store.UserReducer);
    const [userId,setUserId]=useState<string | undefined>();
    const [listMessage, setMessages] = useState<Message[]>([]);
    const paramsUrl=useParams();
  
    
    useEffect(()=>{
      
      setUserId(paramsUrl?.user_id);
    },[paramsUrl?.user_id])

   
    useEffect(()=>{
      if(User.id!=7)
        setUserId((User.id).toString());
        messageService.getAllMassageList().then((response) => {
        setMessages(response)
      })
    },[])
    
    useImperativeHandle(ref, () => ({
      addApi: add
    }))

    const add = (mess: Message) => {
        listMessage.unshift(mess);
      setMessages([...listMessage])
    }

    return (
      <div className="MessageList">
        <Table striped bordered hover variant="dark">
        <thead>
          <tr>
            <th>id </th>
            <th>title</th>
            <th>meesage</th>
          </tr>
        </thead>
        <tbody>{
          listMessage.map((item: Message, index) => {
            if(userId && item.userId==parseInt(userId)){
                return <tr>
                    <td>{index}</td>
                    <td>{item.title}</td>
                    <td>{item.body}</td>
                </tr>
            }
          })}
        </tbody>
      </Table>
        </div>
    )
})

export default MessageCom;

