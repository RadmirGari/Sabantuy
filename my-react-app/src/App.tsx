//import './App.css'
import AdminHome from './HomePages/adminHome';
import Home from './HomePages/home';

function App() {
    const Component = window.location.pathname === '/HomeAdmin'
    ? AdminHome
    : Home

    return <Component />
}

export default App
