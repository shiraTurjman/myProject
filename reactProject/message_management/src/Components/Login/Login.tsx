import { useFormik } from "formik"
import { useDispatch } from "react-redux"
import { useNavigate } from "react-router-dom";



export default function Login() {

    const _dispatch=useDispatch();
    const _navigate=useNavigate();

    const mySubmit = (values:any) => {
        //  submit פונקציה שתופעל בלחיצה על //
        debugger;
        alert("name:" +myFormik.values.name)

       // _dispatch({type:'SET_USER',values})
       const user = {
        name: myFormik.values.name,
        id: myFormik.values.id,
      }

      _dispatch({
        data: user,
        type: "SET_USER",
      });
        _navigate("/header");

    }
    const myCheckValidate=(values:any)=>{
      debugger;
      const errors:any={};
      
      // if(values.name==''||!values.name)
      // errors.name='most'
       if(values.name.length<3)
      errors.name='most by 3 char'
      return errors;
      }
      
    const myFormik = useFormik(
        {
            initialValues: { name: '', id: '' },
            
            validate: myCheckValidate,
            onSubmit: mySubmit,
        }
    )

    
   


    return (
        <>
            <h1>to login</h1>
            <form onSubmit={myFormik.handleSubmit}>
                <div className="form-group">
                    <label htmlFor="name">name</label>
                    <input className="form-control"
                        id="name"
                        name="name"
                        type="name"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.name} />
                </div>
                {myFormik.errors.name ? <div className="alert alert-danger">{myFormik.errors.name}</div>:''}

                <div className="form-group">
                    <label htmlFor="id">id</label>
                    <input className="form-control"
                        id="id"
                        name="id"
                        type="password"
                        onChange={myFormik.handleChange}
                        value={myFormik.values.id} />
                </div>
                <div className="form-group">
                    <button type="submit" className="btn btn-primary">login</button>
                </div>
            </form>

        </>
    )


}