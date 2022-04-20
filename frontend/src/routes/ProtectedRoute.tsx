import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { RootState } from '../redux/store';

const ProtectedRoute = (props: {
  outlet: JSX.Element;
  roles: string[];
}): JSX.Element => {
  const authState = useSelector((state: RootState) => state.auth);

  if (
    authState.isLoggedIn &&
    props.roles.find((role) => role === authState.role)
  ) {
    return props.outlet;
  } else {
    return <Navigate to={{ pathname: '/sign-in' }} />;
  }
};

export default ProtectedRoute;
